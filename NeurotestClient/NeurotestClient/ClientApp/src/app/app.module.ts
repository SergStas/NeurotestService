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
import {EmotionPipe} from "./shared/emotion.pipe";

const routes: Routes = [
  { path: 'client-manager', component: ClientManagerComponent },
  { path: 'config-master', component: ConfigMasterComponent },
  { path: 'test-module', component: TestingModuleComponent }
];

@NgModule({
    declarations: [
        AppComponent,
        ClientFormComponent,
        TestingModuleComponent,
        ClientManagerComponent,
        ConfigMasterComponent,
        QuestionPipe,
        EmotionPipe
    ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ],
  exports: [
    RouterModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
