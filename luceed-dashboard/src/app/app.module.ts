import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DemoComponent } from './pages/demo/demo.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { DohvatiArtikleNazivComponent } from './pages/demo/dohvati-artikle-naziv/dohvati-artikle-naziv.component';
import { DohvatiObracunPoVrstiComponent } from './pages/demo/dohvati-obracun-po-vrsti/dohvati-obracun-po-vrsti.component';
import { DohvatiObracunArtikliComponent } from './pages/demo/dohvati-obracun-artikli/dohvati-obracun-artikli.component';
@NgModule({
  declarations: [
    AppComponent,
    DemoComponent,
    DohvatiArtikleNazivComponent,
    DohvatiObracunPoVrstiComponent,
    DohvatiObracunArtikliComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    HttpClientModule,
    MatCardModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    MatTableModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
