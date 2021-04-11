import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-client-manager',
  templateUrl: './client-manager.component.html',
  styleUrls: ['./client-manager.component.css']
})
export class ClientManagerComponent implements OnInit {
  showMenu = true;
  showClientForm = false;
  displayList = false;

  constructor() { }

  ngOnInit(): void {
  }

  newClient() {
    this.showMenu = false;
    this.showClientForm = true;
  }

  showClients() {

  }
}
