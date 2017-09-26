import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';

import { NavItem } from './navitem.type';

@Injectable()
export class NavMenuService {
    constructor(private http: Http) {

    }

    getNavItems(): NavItem[] {
        return [
            { text: "Home", url: "/" },
            { text: "Cooking", url: "", subItems: [{ text: "Recipe List", url: "/Recipes/Search" }] },
            { text: "About", url: "/About" },
            { text: "Contact", url: "/Home/Contact" },
            { text: "Login", url: "/Login" },
        ] as NavItem[];
    }
}
