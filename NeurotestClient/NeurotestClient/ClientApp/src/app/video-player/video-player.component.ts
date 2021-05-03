import { Component, OnInit } from '@angular/core';
import {NetworkService, VideoInfo, WatchInfo} from "../shared/network.service";
import {VideoConfigService} from "../shared/video-config.service";
import {Router} from "@angular/router";
import {interval, Subscription} from "rxjs";
import {ClientService} from "../shared/client.service";

@Component({
  selector: 'app-video-player',
  templateUrl: './video-player.component.html',
  styleUrls: ['./video-player.component.css']
})
export class VideoPlayerComponent implements OnInit {
  get videoService() {
    return this._videoService;
  }
  get index(): number {
    return this._index;
  }
  get pauseTime(): number {
    return this._videoService.pause - this._pauseTime;
  }
  get isPause(): boolean {
    return this._isPause;
  }
  get end(): boolean {
    return this._end;
  }
  get watchSession(): boolean {
    return this._watchSession;
  }
  get loading(): boolean {
    return this._loading;
  }
  get current() {
    return this._playlist[this._index];
  }

  private _playlist: VideoInfo[] = [];
  private _index = 0;
  private _watchSession = true;
  private _loading = false;
  private _end = false;
  private _api;
  private _currentTime = 0;
  private _pauseTime = 0;
  private _isPause = false;

  private _subscription: Subscription;
  private _currentWatch: WatchInfo = {startTime: null, video: null, endTime: null};
  private _watchResults: WatchInfo[] = [];

  constructor(
    private _networkService: NetworkService,
    private _videoService: VideoConfigService,
    private _clientService: ClientService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._playlist = this._videoService.sessionPlaylist;
  }

  onPlayerReady(api) {
    this._api = api;

    this.setEventsListeners();

    this._watchSession = true;
    this._loading = false;
  }

  private setEventsListeners() {
    this._api.getDefaultMedia().subscriptions.timeUpdate.subscribe(
      data => this.onUpdateTime(data.srcElement.currentTime)
    );
    this._api.getDefaultMedia().subscriptions.loadedMetadata.subscribe(() => {
      if (this._index != 0 && !this._videoService.manualControl)
        this.setPauseTimer();
    })
    this._api.getDefaultMedia().subscriptions.play.subscribe(() => {
      if (!this._currentWatch.startTime)
        this._currentWatch.startTime = VideoPlayerComponent.currentTimestamp().toString();
      if (this._subscription != null)
        this.abortPauseTimer()
    })
    this._api.getDefaultMedia().subscriptions.ended.subscribe(() => {
      this.submitWatchResults()
      this.next();
    })
  }

  private submitWatchResults() {
    this._currentWatch.endTime = VideoPlayerComponent.currentTimestamp().toString();
    this._currentWatch.video = this.current
    this._watchResults.push(this._currentWatch);
    this._currentWatch = {startTime: null, video: null, endTime: null};

    console.log(this._watchResults)
  }

  private static currentTimestamp() {
    return parseInt((Date.now() / 1000).toString())
  }

  private setPauseTimer() {
    this._subscription?.unsubscribe();
    this._isPause = true;
    let pauseTimer = interval(500)
    this._subscription = pauseTimer.subscribe(tick => {
      this._pauseTime = tick / 2;
      if (Math.abs(this._pauseTime - this._videoService.pause) < 1e-5) {
        this.play();
        this._isPause = false;
        this._subscription.unsubscribe();
      }
    })
  }

  private abortPauseTimer() {
    this._subscription.unsubscribe();
    this._isPause = false;
  }

  isConfigsValid() {
    return this._videoService.useLimit != null &&
      this._videoService.pause != null &&
      this._videoService.limit != null &&
      this._videoService.manualControl != null &&
      this._videoService.sessionPlaylist != null
  }

  private play() {
    this._api.play();
  }

  onUpdateTime(currentTime) {
    this._currentTime = currentTime;

    if (!this._videoService.manualControl && this._currentTime >= this._videoService.limit) {
      this.submitWatchResults()
      this.next();
    }
  }

  private next() {
    this._index++;
    if (this._index >= this._playlist.length)
      this.endSession();
  }

  private endSession() {
    this._watchSession = false;
    this._end = true;

    this._networkService.submitResult({
      subjectId: this._clientService.clientId.toString(),
      watchSessions: this._watchResults
    })
  }

  toConfigs() {
    this._router.navigate(['/video-config'])
  }

  toMainMenu() {
    this._router.navigate(['/'])
  }

  switchNext() {
    this.submitWatchResults()
    this.next()
  }
}
