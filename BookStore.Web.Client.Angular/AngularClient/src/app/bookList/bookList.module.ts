import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { CoreModule } from '../@core';
import { BookListComponent } from './bookList.component';

@NgModule({
  imports:
   [
   RouterModule,
    ReactiveFormsModule,
    BrowserModule,
    DataTablesModule,
    HttpClientModule,
    CoreModule,
    NgbModule
  ],
  declarations: [BookListComponent],

})

export class BookListModule { }
