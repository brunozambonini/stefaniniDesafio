import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cidade } from '../models/Cidade';

@Injectable()
export class CidadeService {
  cidadeApiUrl = 'http://localhost:4892/api/Cidades';
  constructor(private http: HttpClient) { }

  getCidades(): Observable<Cidade[]> {
    return this.http.get<Cidade[]>(this.cidadeApiUrl);
  }

  getCidadesFiltering(filter: string): Observable<Cidade[]> {
    return this.http.get<Cidade[]>(`${this.cidadeApiUrl}/${filter}`);
  }

  createCidade(cidade: Cidade): Observable<Cidade> {
    return this.http.post<Cidade>(this.cidadeApiUrl, cidade);
  }

  editCidade(cidade: Cidade): Observable<Cidade> {
    return this.http.put<Cidade>(this.cidadeApiUrl, cidade);
  }

  deleteCidade(id: number): Observable<any> {
    return this.http.delete<any>(`${this.cidadeApiUrl}/${id}`);
  }
}