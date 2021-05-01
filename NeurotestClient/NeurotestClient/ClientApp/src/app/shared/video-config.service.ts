import {Injectable} from "@angular/core";
import {NetworkService, VideoInfo} from "./network.service";
import {ClientService} from "./client.service";
import {Router} from "@angular/router";

@Injectable({providedIn: 'root'})
export class VideoConfigService {
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
  get displayControlType(): boolean {
    return this._displayControlType;
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
  get displaySubmit(): boolean {
    return this._displaySubmit;
  }
  get displayMenu(): boolean {
    return this._displayMenu;
  }

  private _displayMenu = true;
  private _displaySubmit = false;
  private _displayDurationInputs = false;
  private _displayList = false;
  private _displayTypeSelector = false;
  private _displayNameSelector = false;
  private _displayControlType = false;
  private _displayLimitInput = false;
  private _videos: VideoInfo[] = [];
  private _selected: VideoInfo[] = [];
  private _auto: VideoInfo[] = [];

  limit = 60;
  pause = 10;
  private _message: string;
  private _type = '';
  private _useLimit = false;

  constructor(
    private _networkService: NetworkService,
    private _clientService: ClientService,
    private _router: Router
  ) {}

  loadAuto() {
    if (this._clientService.clientId != null) //fixme
      this._message = 'Данные о пациенте отсутсвуют, пожалуйста, проведите сессию тестирования или выберите видео вручную';
    else {
      this._auto = this._networkService.getDefaultVideos(this._clientService.clientId);
      this._displayMenu = false;
      this._displaySubmit = true;
    }
  }

  next() {
    this._router.navigate(['video-player']);
  }

  manual() {
    this._displayMenu = this._displaySubmit = this._displayControlType = false;
    this._displayList = true;

    this._message = '';
    this._videos = this._networkService.getAllVideos();
  }

  setupControls() {
    this._displayList = false;
    this._displayControlType = true;
    this._message = '';
  }

  showMenu() {
    this._displayList = false;
    this._displayMenu = true;
    this._message = '';
  }

  add() {
    this._displayNameSelector = false;
    this._displayTypeSelector = true;
  }

  submitType(emotion: string) {
    this._type = emotion;

    if (!this.filtered.length)
      this._message = 'Видео по этой эмоции не найдено, выберите другую';
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

  selectControlType(type: string) {
    if (type == 'manual') {
      this.pause = -1;
      this.limit = -1;
      this._displayDurationInputs = false;
    }
    else
      this._displayDurationInputs = true;
  }

  toggleLimit() {
    this._displayLimitInput = this._useLimit = !this._useLimit;
  }

  submitConfigs() {
    if (this.validate())
      this.next();
  }

  private validate() {
    if ((this.limit % 1) != 0)
      this._message = 'Лимит должен быть целым числом';
    else if (this.limit < 60)
      this._message = 'Лимит должен быть больше минуты';
    else if ((this.pause % 1) != 0)
      this._message = 'Время паузы должно быть целым числом';
    else if (this.pause < 10 || this.pause > 180)
      this._message = 'Время пазуы должно быть между 10 и 180 секунд';
    else {
      this._message = '';
      return true;
    }
    return false;
  }
}
