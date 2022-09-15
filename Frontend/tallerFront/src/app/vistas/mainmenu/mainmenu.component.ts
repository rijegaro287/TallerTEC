import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mainmenu',
  templateUrl: './mainmenu.component.html',
  styleUrls: ['./mainmenu.component.css']
})
export class MainmenuComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit(): void {
  }

  gestion(){
    this.router.navigate(['dashboard']);
  }
  
}
