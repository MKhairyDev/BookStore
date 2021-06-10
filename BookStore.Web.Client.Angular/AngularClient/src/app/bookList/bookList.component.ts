import { Component, OnInit, ViewChild } from '@angular/core';
import { Routes } from '@angular/router';
import { DataTablesResponse } from '../@shared/classes/data-tables-response';
import { Book } from '../@shared/models/Book';
import { ApiEndpointsService } from '../services/api-endpoints.service';
import { ApiHttpService } from '../services/api-http.service';

@Component({
  selector: 'bookList-root',
  templateUrl: './bookList.component.html',
  styleUrls: ['./bookList.component.css']
})



export class BookListComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  bookList: Book[] = [];

  constructor(private apiHttpService: ApiHttpService, private apiEndpointsService: ApiEndpointsService) {}

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      searching:false,
      ajax: (dataTablesParameters: any, callback) => {
        // Call WebAPI to get book list
        this.apiHttpService
          .get(this.apiEndpointsService.getBooksPagedEndpoint())
          .subscribe((resp: DataTablesResponse) => {
            this.bookList = resp.data;
            callback({
              recordsTotal: resp.data.length,
              recordsFiltered: resp.data.length,
            });
          });
      },
      // Set column title and data field
      columns: [
        {
          title: 'Id',
          data: 'id',
        },
        {
          title: 'Title',
          data: 'title',
        },
        {
          title: 'Description',
          data: 'shortDescription',
        },
        {
          title: 'PublishDate',
          data: 'publishDate',
        },
        {
          title: 'Authors',
          data: 'authors',
        },
      ],
    };
  }
}
