import {Component, OnInit} from '@angular/core';
import {RootPageStateService} from "./shared/root-page-state.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(
    public _rootPageService: RootPageStateService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this._router.navigate(['main-menu'])
  }
}
