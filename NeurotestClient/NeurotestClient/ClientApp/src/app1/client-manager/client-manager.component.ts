import { Component, OnInit } from '@angular/core';
import { ClientService } from '../shared/client.service';

@Component({
  selector: 'app-client-manager',
  templateUrl: './client-manager.component.html',
  styleUrls: ['./client-manager.component.css']
})
export class ClientManagerComponent implements OnInit {
  showMenu = true;
  displayList = false;

  constructor(private clientService: ClientService) { }

  ngOnInit(): void {
  }

  newClient() {
    this.showMenu = false;
    this.clientService.beginClientInit();
  }

  showClients() {
    this.clientService.loadClients();
    this.showMenu = false;
  }
}
