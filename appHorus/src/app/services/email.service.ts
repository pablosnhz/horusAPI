import { Injectable } from '@angular/core';
import * as emailjs from 'emailjs-com';

@Injectable({
  providedIn: 'root'
})
export class EmailService{

  constructor() {
    emailjs.init('NuoqeWE_wxAW37wjW');
  }

  sendEmail(templateParams: any): Promise<void> {
    const serviceID = 'default_service';
    const templateID = 'template_y3zy3fm';

    return new Promise<void>((resolve, reject) => {
      emailjs.send(serviceID, templateID, templateParams)
        .then(() => {
          resolve();
        })
        .catch((error) => {
          reject(error);
        });
    });
  }

}
