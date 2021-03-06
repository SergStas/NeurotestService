import { Component, OnInit } from '@angular/core';
import {VideoConfigService} from "../shared/video-config.service";
import {NetworkService, VideoInfo} from "../shared/network.service";
import {ClientService} from "../shared/client.service";
import {Router} from "@angular/router";
import { type } from 'os';

@Component({
  selector: 'app-video-config',
  templateUrl: './video-config.component.html',
  styleUrls: ['./video-config.component.css']
})
export class VideoConfigComponent implements OnInit {
  get auto(): VideoInfo[] {
    return this._auto;
  }
  get displayLimitInput(): boolean {
    return this._displayLimitInput;
  }
  get displayDurationInputs(): boolean {
    return this._displayDurationInputs;
  }
  get selected(): VideoInfo[] {
    return this._selected;
  }
  get message(): string {
    return this._message;
  }
  get filtered(): VideoInfo[] {
    return this._videos.filter(v => v.type == this._type && !this.selected.find(s => v.name == s.name));
  }
  get displayControlConfig(): boolean {
    return this._displayControlConfig;
  }
  get displayNameSelector(): boolean {
    return this._displayNameSelector;
  }
  get displayTypeSelector(): boolean {
    return this._displayTypeSelector;
  }
  get displayList(): boolean {
    return this._displayList;
  }
  get displayAutoload(): boolean {
    return this._displayAutoload;
  }
  get displayMenu(): boolean {
    return this._displayMenu;
  }

  limit = 60;
  pause = 10;

  private _displayMenu = true;
  private _displayAutoload = false;
  private _displayDurationInputs = false;
  private _displayList = false;
  private _displayTypeSelector = false;
  private _displayNameSelector = false;
  private _displayControlConfig = false;
  private _displayLimitInput = false;
  private _videos: VideoInfo[] = [];
  private _selected: VideoInfo[] = [];
  private _auto: VideoInfo[] = [];
  private _message: string;

  private _type = '';
  private _useLimit = false;

  constructor(
    private _networkService: NetworkService,
    private _clientService: ClientService,
    private _videoConfigService: VideoConfigService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    if (this._clientService.clientId == null) {
      this._message = '???????????? ?? ???????????????? ????????????????????, ????????????????????, ?????????????????? ???????????? ????????????????????????';
      this._displayControlConfig =
        this._displayList =
        this._displayDurationInputs =
        this._displayLimitInput =
        this._displayAutoload =
        this._displayNameSelector =
        this._displayMenu =
        this._displayTypeSelector = false;
    }
  }

  showAutoload() {
      this._displayMenu =
      this._displayControlConfig =
      this._displayDurationInputs =
      this._displayLimitInput =
      this._displayList =
      this._displayNameSelector =
      this._displayTypeSelector = false;
    this._displayAutoload = true;
    this._message = '';

    this._networkService.getDefaultVideos(this._clientService.clientId).subscribe(
      (data: { url: string, type: string }[]) => this._auto = data.map(o => this.convertToVideoInfo(o))
    );
  }

  goToPlayer() {
    this._videoConfigService.sessionPlaylist = this._selected.length ? this._selected : this._auto;
    this._videoConfigService.limit = this.limit;
    this._videoConfigService.pause = this.pause;
    this._router.navigate(['video-player']);
  }

  showManualSelector() {
    this._displayMenu =
      this._displayControlConfig =
      this._displayDurationInputs =
      this._displayLimitInput =
      this._displayAutoload =
      this._displayNameSelector =
      this._displayTypeSelector = false;
    this._displayList = true;
    this._message = '';

    this._networkService.getAllVideos().subscribe(
      (data: { url: string, type: string }[]) => this._videos = data.map(o => this.convertToVideoInfo(o))
    );
  }

  showControlConfig() {
    this._displayMenu =
      this._displayList =
      this._displayDurationInputs =
      this._displayLimitInput =
      this._displayAutoload =
      this._displayNameSelector =
      this._displayTypeSelector = false;
    this._displayControlConfig = true;
    this._message = '';

    this.limit = 60;
    this.pause = 10;
    this._useLimit = this._videoConfigService.useLimit = false;

    this.setControlType('manual');
  }

  showMenu() {
    if (this._clientService.clientId == null) {
      this._message = '???????????? ?? ???????????????? ????????????????????, ????????????????????, ?????????????????? ???????????? ????????????????????????';
      this._displayControlConfig =
        this._displayList =
        this._displayDurationInputs =
        this._displayLimitInput =
        this._displayAutoload =
        this._displayNameSelector =
        this._displayMenu =
        this._displayTypeSelector = false;
    }
    else {
      this._displayControlConfig =
        this._displayList =
        this._displayDurationInputs =
        this._displayLimitInput =
        this._displayAutoload =
        this._displayNameSelector =
        this._displayTypeSelector = false;
      this._displayMenu = true;
      this._message = '';
    }
  }

  add() {
    this._displayNameSelector = false;
    this._displayTypeSelector = true;
  }

  submitType(emotion: string) {
    this._type = emotion;

    if (!this.filtered.length)
      this._message = '?????????? ???? ???????? ???????????? ???? ??????????????, ???????????????? ????????????';
    else {
      this._displayTypeSelector = false;
      this._displayNameSelector = true;
      this._message = '';
    }
  }

  chooseVideo(name: string) {
    this._selected.push(this._videos.find(v => v.name == name));

    this._displayNameSelector = false;
  }

  delete(name: string) {
    this._selected = this._selected.filter(v => v.name != name);
  }

  setControlType(type: string) {
    this._displayDurationInputs = type == 'auto';
    this._videoConfigService.manualControl = type == 'manual';
  }

  toggleLimit() {
    this._displayLimitInput = this._videoConfigService.useLimit = this._useLimit = !this._useLimit;
  }

  submitConfigs() {
    if (this.validate())
      this.goToPlayer();
  }

  private validate() {
    this._message =
      this._videoConfigService.manualControl ? '' :
      (this.limit % 1) != 0 ? '?????????? ???????????? ???????? ?????????? ????????????' :
      this.limit < 60 ? '?????????? ???????????? ???????? ???????????? ????????????' :
      (this.pause % 1) != 0 ? '?????????? ?????????? ???????????? ???????? ?????????? ????????????' :
      this.pause < 10 || this.pause > 180 ? '?????????? ?????????? ???????????? ???????? ?????????? 10 ?? 180 ????????????' :
      '';

    return this._message == '';
  }

  private convertToVideoInfo(obj: { type: string, url: string }): VideoInfo {
    return {
      url: obj.url,
      type: obj.type,
      name: obj.url.split('/')[obj.url.split('/').length - 1]
    }
  }
}
