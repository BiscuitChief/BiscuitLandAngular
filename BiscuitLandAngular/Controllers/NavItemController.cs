﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;


namespace BiscuitLandAngular.Controllers
{
    public class NavItemController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public NavItemController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("api/gettopnavigation")]
        [HttpGet]
        public IActionResult GetTopNavigation()
        {
            List<Models.NavItem> navItemList = new List<Models.NavItem>();

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Path.Combine(_hostingEnvironment.WebRootPath, "App_Data\\Xml\\TopNavigation.xml"));
            foreach (XmlNode menunode in xmldoc.SelectNodes("//TopNavigation/MenuItem"))
            {
                ProcessTopNavigationNode(navItemList, menunode);
            }

            return Ok(navItemList);
        }

        private void ProcessTopNavigationNode(List<Models.NavItem> navItemList, XmlNode menunode)
        {
            //declare variables
            string nodetext = String.Empty;
            string nodeurl = String.Empty;
            string nodetarget = String.Empty;
            bool showitem = false;
            List<string> noderoles = new List<string>();

            //populate variables
            try
            { nodetext = menunode.SelectSingleNode("Text").InnerText.Trim(); }
            catch { nodetext = String.Empty; }
            try
            { nodeurl = menunode.SelectSingleNode("Url").InnerText.Trim(); }
            catch { nodeurl = String.Empty; }
            try
            { nodetarget = menunode.SelectSingleNode("Target").InnerText.Trim(); }
            catch { nodetarget = String.Empty; }
            try
            {
                //Get the list of security roles that are allowed to view the menu item
                XmlNodeList rolenodes = menunode.SelectNodes("SecurityRole");
                if (rolenodes.Count > 0)
                {
                    foreach (XmlNode rn in rolenodes)
                    { noderoles.Add(rn.InnerText); }
                }
            }
            catch { noderoles = new List<string>(); }

            //If no roles listed anyone can see it
            if (noderoles.Count == 0)
            { showitem = true; }
            else
            {
                //If user has access to at least one role they can see the item
                foreach (string role in noderoles)
                {
                    if (User.IsInRole(role))
                    {
                        showitem = true;
                        break;
                    }
                }
            }

            if (menunode.Attributes["Authenticated"] != null)
            {
                if (menunode.Attributes["Authenticated"].Value == "0")
                { showitem = showitem && !User.Identity.IsAuthenticated; }
                else if (menunode.Attributes["Authenticated"].Value == "1")
                { showitem = showitem && User.Identity.IsAuthenticated; }
            }

            //Ignore items with blank text or if the user does not have access
            if (showitem && !String.IsNullOrEmpty(nodetext))
            {
                Models.NavItem newitem = new Models.NavItem()
                {
                    Text = nodetext,
                    Url = nodeurl,
                    Target = nodetarget,
                    SubItems = new List<Models.NavItem>()
                };
                //Loop through any child nodes
                XmlNodeList childnodes = menunode.SelectNodes("MenuItem");
                if (childnodes.Count > 0)
                {
                    foreach (XmlNode nextnode in childnodes)
                    { ProcessTopNavigationNode(newitem.SubItems, nextnode); }
                }

                navItemList.Add(newitem);
            }
        }
    }
}
