
import { Component, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.css'],
  animations: [
    trigger('buttonState', [
      state('primary', style({
        background: '#1976D2' // primary color
      })),
      state('accent', style({
        background: '#FF4081' // accent color
      })),
      transition('* => *', [
        animate('0.3s')
      ])
    ])
  ]
})
export class PageComponent implements OnInit {
  ngOnInit() {
  }
}
