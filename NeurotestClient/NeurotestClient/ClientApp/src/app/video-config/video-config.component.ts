import { Component, OnInit } from '@angular/core';
import {VideoConfigService} from "../shared/video-config.service";

@Component({
  selector: 'app-video-config',
  templateUrl: './video-config.component.html',
  styleUrls: ['./video-config.component.css']
})
export class VideoConfigComponent{

  constructor(public configService: VideoConfigService) { }
}
