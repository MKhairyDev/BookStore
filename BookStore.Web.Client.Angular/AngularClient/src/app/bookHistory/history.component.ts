import { Component, OnInit, ViewChild } from '@angular/core';
import { Routes } from '@angular/router';
import { DataTablesResponse } from '../@shared/classes/data-tables-response';
import { BookHistory } from '../@shared/models/BookHistory';
import { ApiEndpointsService } from '../services/api-endpoints.service';
import { ApiHttpService } from '../services/api-http.service';

@Component({
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})



export class HistoryComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  bookHistory: BookHistory[] = [];

  constructor(private apiHttpService: ApiHttpService, private apiEndpointsService: ApiEndpointsService) {}

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback) => {
        // Call WebAPI to get book history
        this.apiHttpService
          .post(this.apiEndpointsService.postBookHistoryPagedEndpoint(), dataTablesParameters)
          .subscribe((resp: DataTablesResponse) => {
            this.bookHistory = resp.data;
            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: [],
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
          title: 'Book number',
          data: 'bookId',
        },
        {
          title: 'Action',
          data: 'action',
        },
        {
          title: 'Description',
          data: 'description',
        }
      ],
    };
  }
}
