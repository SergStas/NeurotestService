import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import {Router} from "@angular/router";
import { Answer } from '../shared/network.service';
import {TestingService} from "../shared/testing.service";
import {RootPageStateService} from "../shared/root-page-state.service";

@Component({
  selector: 'app-testing-module',
  templateUrl: './testing-module.component.html',
  styleUrls: ['./testing-module.component.css']
})
export class TestingModuleComponent implements OnInit{
  constructor(
    public testingService: TestingService,
    public router: Router,
    private rootPageService: RootPageStateService
  ) {
    router.events.subscribe(_ => rootPageService.displayHeader())
  }

  @ViewChild('input')
  private buttons: ElementRef;

  beginSession() {
    this.testingService.startTest();
    this.rootPageService.hideHeader();

    setTimeout(() => {
      this.buttons.nativeElement.focus();
    }, 0);
  }

  abort() {
    this.testingService.abortTest();
    this.rootPageService.displayHeader();
  }

  back() {
    this.abort();
    this.router.navigate(['config-master']);
  }

  ngOnInit(): void {
    this.testingService.setup();
  }

  rerun() {
    this.rootPageService.displayHeader();
    this.testingService.setup();
  }

  getElapsed(answer: Answer) {
    return Number.parseFloat(answer.ElapsedTime);
  }

  toVideo() {
    this.router.navigate(['/video-config'])
  }
}
