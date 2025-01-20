import { Component } from '@angular/core';
import { EmailService } from 'src/app/services/email.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent {



  formData = {
    email: '',
    message: ''
  };

  constructor(private emails: EmailService) { }

  onSubmit(): void {
    const templateParams = {
      email_id: this.formData.email,
      message: this.formData.message
    };

    this.emails.sendEmail(templateParams)
      .then(() => {
        alert('Email enviado!');
      })
      .catch((error) => {
        alert('Error al enviar el email: ' + JSON.stringify(error));
      });
  }

  }

