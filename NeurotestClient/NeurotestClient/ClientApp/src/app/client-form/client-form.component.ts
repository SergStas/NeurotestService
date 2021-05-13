import { Component, OnInit } from '@angular/core';
import {SubjectInfo, ClientService} from "../shared/client.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css']
})
export class ClientFormComponent implements OnInit {
  get jobs(): string[] {
    return this._jobs;
  }
  public errorMessage = '';

  public lastName = '';
  public firstName = '';
  public secondName = '';

  public sex = 'Male';
  public birthDate: string;
  public address = '';
  public job = '';
  public diseases = '';
  public phoneNumber = '';

  public client: SubjectInfo;

  private _jobs = [
    "Вупсень",
    "Пупсень",
    "Другое"
  ]

  constructor(private clientService: ClientService, private router: Router) { }

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
    else if (!this.secondName.trim())
      this.errorMessage = 'Пожалуйста, укажите отчество'
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
      LastName: this.lastName,
      FirstName: this.firstName,
      Patronymic: this.secondName,
      Sex: this.sex,
      BirthDate: this.birthDate,
      Address: this.address,
      Job: this.job,
      Diseases: this.diseases,
      Phone: this.phoneNumber
    };

    this.clientService.submitNewClient(this.client);
    this.router.navigate(['/config-master']);
  }

  private checkPhoneNumber() {
    return this.phoneNumber.match('^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$');
  }
}
