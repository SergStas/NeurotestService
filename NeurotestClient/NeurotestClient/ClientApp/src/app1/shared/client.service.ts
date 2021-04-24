import {Injectable} from "@angular/core";
import {NetworkService} from "./network.service";

export class SubjectInfo {
  LastName: string;
  FirstName: string;
  Patronymic: string;
  Sex: string;
  BirthDate: string;
  Address: string;
  Job: string;
  Diseases: string;
  Phone: string;
}

@Injectable({providedIn: "root"})
export class ClientService {
  public clients: SubjectInfo[];

  private sessionClient: SubjectInfo
  get client() {
    return this.sessionClient;
  }

  private id: number
  get clientId() {
    return this.id;
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

  selectClient(client: SubjectInfo) {
    this.sessionClient = client;
    this.selectClientSession = false;
  }

  submitNewClient(client: SubjectInfo) {
    this.sessionClient = client;
    this.newClientSession = false;

    this.networkService.addClient(client)
      .subscribe((id: number) => this.id = id);
  }

  loadClients() {
    this.selectClientSession = true;
    this.networkService.getClients().subscribe(
      (subjects: SubjectInfo[]) => this.clients = subjects
    );
  }
}
