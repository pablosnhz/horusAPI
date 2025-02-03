import { Component, OnInit } from '@angular/core';
import { EmailService } from 'src/app/services/email.service';
import { ProductsService } from 'src/app/services/productos.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit{

  ngOnInit(): void {
    // this.returnData();
  }

  // returnData(){
  //   console.log('Productos de .NET');
  //   this.productsService.apiMerch().subscribe((data) => {
  //     console.log(data);
  //   });
  // }

  formData = {
    email: '',
    message: ''
  };

  constructor(private emails: EmailService, private productsService: ProductsService) { }

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

