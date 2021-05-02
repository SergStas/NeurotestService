import {Injectable} from "@angular/core";
import {VideoInfo} from "./network.service";

@Injectable({providedIn: 'root'})
export class VideoConfigService {
  get useLimit(): boolean {
    return this._useLimit;
  }
  set useLimit(value: boolean) {
    this._useLimit = value;
  }
  get limit(): number {
    return this._limit;
  }
  set limit(value: number) {
    this._limit = value;
  }
  get pause(): number {
    return this._pause;
  }
  set pause(value: number) {
    this._pause = value;
  }
  get manualControl(): boolean {
    return this._manualControl;
  }
  set manualControl(value: boolean) {
    this._manualControl = value;
  }
  get sessionPlaylist(): VideoInfo[] {
    return this._sessionPlaylist;
  }
  set sessionPlaylist(value: VideoInfo[]) {
    this._sessionPlaylist = [];
    value.forEach(v => this._sessionPlaylist.push(v));
  }

  private _sessionPlaylist: VideoInfo[];
  private _manualControl: boolean;
  private _pause: number;
  private _limit: number;
  private _useLimit: boolean;
}
