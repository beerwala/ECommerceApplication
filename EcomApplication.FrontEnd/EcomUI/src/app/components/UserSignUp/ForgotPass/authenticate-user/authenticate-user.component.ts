import { Component } from '@angular/core';

@Component({
  selector: 'app-authenticate-user',
  templateUrl: './authenticate-user.component.html',
  styleUrls: ['./authenticate-user.component.css']
})
export class AuthenticateUserComponent {
  email: string = ''; // Variable to store the email
    // Method to open the popup and set the email
    openPopup(email: string) {
      this.email = email;
      //document.getElementById('popup').style.display = 'block';
    }
  closePopup() {
    //document.getElementById('popup').style.display = 'none';
  }
}
