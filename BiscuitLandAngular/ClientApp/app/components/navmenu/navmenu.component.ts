import { Component, OnInit } from '@angular/core';
import { NavItem } from './navitem.type';
import { NavMenuService } from './navmenu.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {

    navItemList: NavItem[];

    constructor(private navMenuService: NavMenuService) {
    }

    ngOnInit() {
        this.navMenuService.getNavItems().then(navitems => { this.navItemList = navitems; });
    }

}
