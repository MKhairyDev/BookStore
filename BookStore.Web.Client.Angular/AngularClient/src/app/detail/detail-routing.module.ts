import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DetailComponent } from './detail.component';

const routes: Routes = [
  // Module is lazy loaded, see app-routing.module.ts
  { path: 'detail', component: DetailComponent, data: { title: 'Detail'} },
  { path: ':id', component: DetailComponent, data: { title: 'Detail' } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class DetailRoutingModule {}
