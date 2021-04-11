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
  private sessionClient: Client

  addClient(client: Client) {

  }
}
