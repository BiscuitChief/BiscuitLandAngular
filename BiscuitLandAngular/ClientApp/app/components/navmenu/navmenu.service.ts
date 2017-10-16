import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';

import { NavItem } from './navitem.type';

@Injectable()
export class NavMenuService {
    constructor(private http: Http) {

    }

    getNavItems(): Promise<NavItem[]> {
        return this.http.get('api/gettopnavigation')
            .toPromise()
            .then(response => response.json() as NavItem[]);
    }
}
