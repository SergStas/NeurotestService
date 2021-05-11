import { Component } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.css']
})
export class MainMenuComponent {

  constructor(private _router: Router) {  }

  toForm() {
    this._router.navigate(['client-form']);
  }
}
