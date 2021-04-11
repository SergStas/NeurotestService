import {Injectable} from "@angular/core";

export class Client {
  lastName: string;
  firstName: string;
  secondName?: string;
  sex: string;
  birthDate: string;
  address: string;
  job: string;
  diseases: string;
  phoneNumber: string;
}

@Injectable({providedIn: "root"})
export class ClientService {
  public clients: Client[] = [];

  private sessionClient: Client

  private newClientSession = false;
  private selectClientSession = false;

  isNewClientSession() {
    return this.newClientSession;
  }

  isSelectClientSession() {
    return this.selectClientSession;
  }

  beginClientInit() {
    this.newClientSession = true;
  }

  selectClient(client: Client) {
    this.sessionClient = client;
    this.selectClientSession = false;
  }

  submitNewClient(client: Client) {
    this.sessionClient = client;
    this.newClientSession = false;
  }

  loadClients() {
    //TODO

    this.clients = [
      { firstName: 'as', lastName: 'as', address: 'as', birthDate: 'as', diseases: 'as', sex: 'as', job: 'as', phoneNumber: 'as' },
      { firstName: 'as', lastName: 'as', address: 'as', birthDate: 'as', diseases: 'as', sex: 'as', job: 'as', phoneNumber: 'as' }
    ];
  }
}
