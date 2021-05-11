import {Injectable} from "@angular/core";

@Injectable({providedIn: 'root'})
export class RootPageStateService {
  get isHeaderDisplayed() {
    return this._header;
  }
  private _header = true;

  hideHeader() {
    this._header = false;
  }

  displayHeader() {
    this._header = true;
  }
}
