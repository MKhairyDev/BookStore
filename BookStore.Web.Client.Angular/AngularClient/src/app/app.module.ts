import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DataTablesModule } from 'angular-datatables';
import { CoreModule } from './@core';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { DetailModule } from './detail/detail.module';
import { DetailComponent } from './detail/detail.component';
import { HistoryComponent } from './bookHistory/history.component';
import { HistoryModule } from './bookHistory/history.module';
import { BookListModule } from './bookList/bookList.module';
import { BookListComponent } from './bookList/bookList.component';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    RouterModule.forRoot([
      { path: '', component: BookListComponent },
      { path: 'history/all', component: HistoryComponent },
      { path: 'detail', component: DetailComponent },
      { path: ':id', component: DetailComponent },
    ]),
    BookListModule,
    HistoryModule,
    DetailModule,
    BrowserModule,
    DataTablesModule,
    HttpClientModule,
    CoreModule,
    NgbModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
