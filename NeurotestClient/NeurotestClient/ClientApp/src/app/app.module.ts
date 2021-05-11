import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { ClientFormComponent } from './client-form/client-form.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { TestingModuleComponent } from './testing-module/testing-module.component';
import { ClientManagerComponent } from './client-manager/client-manager.component';
import { ConfigMasterComponent } from './config-master/config-master.component';
import {QuestionPipe} from "./shared/question.pipe";
import { EmotionPipe } from "./shared/emotion.pipe";
import { HttpClientModule } from "@angular/common/http";
import { VideoConfigComponent } from './video-config/video-config.component';
import { VideoPlayerComponent } from './video-player/video-player.component';

import { VgCoreModule } from '@videogular/ngx-videogular/core';
import { VgControlsModule } from '@videogular/ngx-videogular/controls';
import { VgOverlayPlayModule } from '@videogular/ngx-videogular/overlay-play';
import { VgBufferingModule } from '@videogular/ngx-videogular/buffering';
import { MainMenuComponent } from './main-menu/main-menu.component';

const routes: Routes = [
  { path: 'client-manager', component: ClientManagerComponent },
  { path: 'client-form', component: ClientFormComponent },
  { path: 'config-master', component: ConfigMasterComponent },
  { path: 'test-module', component: TestingModuleComponent },
  { path: 'video-config', component: VideoConfigComponent },
  { path: 'video-player', component: VideoPlayerComponent },
  { path: 'main-menu', component: MainMenuComponent }
];

@NgModule({
    declarations: [
        AppComponent,
        ClientFormComponent,
        TestingModuleComponent,
        ClientManagerComponent,
        ConfigMasterComponent,
        QuestionPipe,
        EmotionPipe,
        VideoConfigComponent,
        VideoPlayerComponent,
        MainMenuComponent
    ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    VgCoreModule,
    VgOverlayPlayModule,
    VgControlsModule,
    VgBufferingModule
  ],
  exports: [
    RouterModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
