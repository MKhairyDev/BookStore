import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailRoutingModule } from './detail-routing.module';
import { DetailComponent } from './detail.component';

import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';


@NgModule({
  imports: [
    RouterModule,
    CommonModule,
    DetailRoutingModule,
    ReactiveFormsModule,
  ],
  declarations: [DetailComponent],
})
export class DetailModule {}
