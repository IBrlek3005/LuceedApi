import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';
import { catchError, of, take, tap } from 'rxjs';
import { ArtiklObracun } from '../models/artikl-obracun-model';

@Component({
  selector: 'app-dohvati-obracun-artikli',
  templateUrl: './dohvati-obracun-artikli.component.html',
  styleUrls: ['./dohvati-obracun-artikli.component.scss']
})
export class DohvatiObracunArtikliComponent implements OnInit {
  public dataSource: ArtiklObracun[] = []
  displayedColumns: string[] = ['artikl_uid', 'naziv_artikla', 'kolicina', 'iznos', 'usluga'];
  public obracunForm!: FormGroup
  
  constructor(
    private _httpClient: HttpClient,
    private _formBuilder: FormBuilder,
    private _messageService: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.obracunForm = this._formBuilder.group({
      id: ['', Validators.required],
      odDatuma: ['', Validators.required],
      doDatuma: [''],
    });
  }

  onSubmit(): void {
    this.obracunForm.markAllAsTouched();
    if (!this.obracunForm.valid) {
      this._messageService.open(
        'Niste unijeli obavezan podatak',
        'U redu',
        {
          duration: 1000
        }
      );
      return;
    }

    this._httpClient.get(`${environment.backend}/api/Luceed/GetObracunArtikli/${this.obracunForm.get('id')!.value}/${this.obracunForm.get('odDatuma')!.value}/${this.obracunForm.get('doDatuma')!.value}`)
    .pipe(
      take(1),
      tap((response: any) => {this.dataSource = response
      console.log(this.dataSource, response)}),
      catchError(error =>  of(error))
    ).subscribe();
  }
}
