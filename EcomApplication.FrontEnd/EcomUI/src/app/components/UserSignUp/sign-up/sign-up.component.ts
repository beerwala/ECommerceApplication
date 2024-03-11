import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  user1 = {
    firstName: '',
    lastName: '',
    email: '',
    userType: '',
    dob: '',
    username: '',
    password: '',
    mobile: '',
    address: '',
    zipcode: null,
    profileImage: '',
    state: '',
    country: ''
  };
  user: any = {};
  states: { id: number, name: string }[] = [
    { id: 1, name: 'State 1' },
    { id: 2, name: 'State 2' },
    { id: 3, name: 'State 3' },
    // Add more states as needed
  ];

  countries: { id: number, name: string }[] = [
    { id: 1, name: 'Country 1' },
    { id: 2, name: 'Country 2' },
    { id: 3, name: 'Country 3' },
    // Add more countries as needed
  ];
  
  onFileSelected(event: any) {
    const file: File = event.target.files[0]; // Get the selected file
  
    // Create a new FileReader
    const reader = new FileReader();
    reader.onload = () => {
      // Convert the file to a base64 data URL
      const base64DataUrl: string = reader.result as string;
      console.log('Base64 data URL:', base64DataUrl);
  
      this.user.profileImage = base64DataUrl;
    };
  
    // Read the selected file as a data URL
    reader.readAsDataURL(file);
  }
  
  
  onSubmit(form: NgForm) {
    if (form.valid) {
      // Form is valid, handle form submission logic here
      console.log('Form submitted:', this.user);
    } else {
      // Form is invalid, display validation errors
      console.log('Form is invalid. Please fix errors.');
    }
  }

}
