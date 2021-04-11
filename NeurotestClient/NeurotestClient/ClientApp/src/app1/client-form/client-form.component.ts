import { Component, OnInit } from '@angular/core';
import {Client, ClientService} from "../shared/client.service";

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css']
})
export class ClientFormComponent implements OnInit {
  public errorMessage = '';

  public lastName = '';
  public firstName = '';
  public secondName = '';

  public sex = '';
  public birthDate: string;
  public address = '';
  public job = '';
  public diseases = '';
  public phoneNumber = '';

  public client: Client;

  constructor(private clientService: ClientService) { }

  ngOnInit(): void {
  }

  confirm() {
    if (this.validate())
      this.submitClient();
  }

  setSex(sex: string) {
    this.sex = sex;
  }

  private validate(): boolean {
    if (!this.lastName.trim())
      this.errorMessage = 'Пожалуйста, укажите фамилию'
    else if (!this.firstName.trim())
      this.errorMessage = 'Пожалуйста, укажите имя'
    else if (!this.birthDate)
      this.errorMessage = 'Пожалуйста, укажите дату рождения'
    else if (!this.address)
      this.errorMessage = 'Пожалуйста, укажите адрес'
    else if (!this.job)
      this.errorMessage = 'Пожалуйста, укажите род деятельности'
    else if (!this.diseases)
      this.errorMessage = 'Пожалуйста, укажите заболевания'
    else if (!this.phoneNumber)
      this.errorMessage = 'Пожалуйста, укажите номер телефона'
    else if (!this.checkPhoneNumber())
      this.errorMessage = 'Неверный формат телефона (образец - +7ХХХХХХХХХХ)'
    else {
      this.errorMessage = '';
      return true;
    }
    return false;
  }

  private submitClient() {
    this.client = {
      lastName: this.lastName,
      firstName: this.firstName,
      secondName: this.secondName ? this.secondName : null,
      sex: this.sex,
      birthDate: this.birthDate,
      address: this.address,
      job: this.job,
      diseases: this.diseases,
      phoneNumber: this.phoneNumber
    };

    this.clientService.submitNewClient(this.client);
  }

  private checkPhoneNumber() {
    return this.phoneNumber.match('^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$');
  }
}
