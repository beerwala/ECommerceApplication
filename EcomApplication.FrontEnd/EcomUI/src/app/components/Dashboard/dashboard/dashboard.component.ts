import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  constructor(private router: Router) { }

  ngOnInit(): void {
    // Fetch profile picture and cart count logic can go here
  }

  editProfile(): void {
    // Navigate to the edit profile page
    this.router.navigate(['/edit-profile']);
  }
}
