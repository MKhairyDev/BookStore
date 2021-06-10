import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApiEndpointsService } from '../services/api-endpoints.service';
import { ApiHttpService } from '../services/api-http.service';
import { Book } from '../@shared/models/Book';
import { DataResponseBook } from '../@shared/classes/DataResponseBook';
import { ToastService } from '../services/toast.service';


@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
})
export class DetailComponent implements OnInit {
  formMode = 'New';
  sub: any;
  id: number=0;
  entryForm!: FormGroup;
  error: string | undefined;
  book!: Book;
  isAddNew: boolean = false;
  authorsList: any = [{id:1,name:"Uncle Bob"}, {id:2,name:"Martin Fowler"},{id:3,name:"Martin Kleppmann"}];
  selectedAuthors: any[] = [];
  constructor(
    public toastService: ToastService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private apiHttpService: ApiHttpService,
    private apiEndpointsService: ApiEndpointsService,
  ) {
    this.createForm();
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe((params) => {
      this.id = params['id'];
      if (this.id !== undefined) {
        this.read(this.route.snapshot.paramMap.get('id'));
        this.formMode = 'Edit';
      } else {
        this.isAddNew = true;
        this.formMode = 'New';
      }
    });
  }

  // Handle Create button click
  onCreate() {
    this.create(this.entryForm.value);
  }

  // Handle Update button click
  onUpdate() {
    this.put(this.entryForm.get('id')!.value, this.entryForm.value);
    alert("Data is updated successfully");
    // this.showSuccess('Update', 'Data is updated');
  }


  // CRUD > Read, map to REST/HTTP GET
  read(id: any): void {
    this.apiHttpService.get(this.apiEndpointsService.getBookByIdEndpoint(id), id).subscribe(
      //Assign resp to class-level model object.
      (resp: DataResponseBook) => {
        //Assign data to class-level model object.
        this.book = resp.data;
        //Populate reactive form controls with model object properties.
        this.entryForm.setValue({
          id: this.book.id,
          title: this.book.title,
          shortDescription: this.book.shortDescription,
         authors: this.book.authors,
        });
        // this.entryForm.controls.authors.patchValue(this.book.authors);

      },
      (error) => {
        //Log errer
      }
    );
  }

  // CRUD > Create, map to REST/HTTP POST
  create(data: any): void {
    data.id=0;
    this.apiHttpService.post(this.apiEndpointsService.postBooksEndpoint(), data).subscribe((resp: any) => {
      this.id = resp.data.id; //guid return in data

      alert("Data is inserted successfully");
      // this.showSuccess('Creation', 'Data is inserted');
     this.entryForm.reset();
    });
  }

  // CRUD > Update, map to REST/HTTP PUT
  put(id: string, data: any): void {
    this.apiHttpService.put(this.apiEndpointsService.putBooksEndpoint(id), data).subscribe((resp: any) => {
      this.id = resp.data.id; //guid return in data
    });
  }

  // reactive form
  private createForm() {
    this.entryForm = this.formBuilder.group({
      id: 0,
      title: ['', Validators.required],
      shortDescription: ['', Validators.required],
     authors: ['', Validators.required],
     
     
    });
  }
  // ngbmodal service
  showSuccess(headerText: string, bodyText: string) {
    this.toastService.show(bodyText, {
      classname: 'bg-success text-light',
      delay: 2000,
      autohide: true,
      headertext: headerText,
    });
  }
    // Choose city using select dropdown
    changeCity(e:any) {
      console.log(e.value)
      this.selectedAuthors.push(e.target.value.id, {
        onlySelf: true
      })
    }
}
