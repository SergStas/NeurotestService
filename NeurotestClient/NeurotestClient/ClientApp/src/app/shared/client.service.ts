import {Injectable} from "@angular/core";
import {NetworkService} from "./network.service";

export class Client {
  id?: number;
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
  get client() {
    return this.sessionClient;
  }

  private newClientSession = false;
  private selectClientSession = false;

  public constructor(
    private networkService: NetworkService
  ) {  }

  abortSession() {
    this.selectClientSession = false;
    this.newClientSession = false;
  }

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

    this.networkService.addClient(client);
  }

  loadClients() {
    this.selectClientSession = true;

    this.clients = this.networkService.getClients();
  }
}
