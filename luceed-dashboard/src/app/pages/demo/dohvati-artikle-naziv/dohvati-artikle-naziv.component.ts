import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Artikl } from '../models/artiki-model';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';
import { catchError, of, take, tap } from 'rxjs';

@Component({
  selector: 'app-dohvati-artikle-naziv',
  templateUrl: './dohvati-artikle-naziv.component.html',
  styleUrls: ['./dohvati-artikle-naziv.component.scss']
})
export class DohvatiArtikleNazivComponent implements OnInit {
  public artikliForm!: FormGroup;
  public dataSource: Artikl[]= [];
  displayedColumns: string[] = ['id', 'naziv', 'sid'];


  constructor(
    private _httpClient: HttpClient,
    private _formBuilder: FormBuilder,
    private _messageService: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.artikliForm = this._formBuilder.group({
      naziv: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.artikliForm.markAllAsTouched();
    if (!this.artikliForm.valid) {
      this._messageService.open(
        'Niste unijeli obavezan podatak',
        'U redu',
        {
          duration: 1000
        }
      );
      return;
    }

    this._httpClient.get(`${environment.backend}/api/Luceed/GetArtikle/${this.artikliForm.get('naziv')!.value}`)
    .pipe(
      take(1),
      tap((response: any) => {
        this.dataSource = response;
      }),
      catchError(error =>  of(error))
    ).subscribe();
  }
}
