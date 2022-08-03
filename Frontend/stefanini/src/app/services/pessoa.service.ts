import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pessoa } from '../models/Pessoa';

@Injectable()
export class PessoaService {
  pessoaApiUrl = 'http://localhost:4892/api/Pessoas';
  constructor(private http: HttpClient) { }

  getPessoas(): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(this.pessoaApiUrl);
  }

  getPessoasFiltering(filter: string): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(`${this.pessoaApiUrl}/${filter}`);
  }

  createPessoa(pessoa: Pessoa): Observable<Pessoa> {
    return this.http.post<Pessoa>(this.pessoaApiUrl, pessoa);
  }

  editPessoa(pessoa: Pessoa): Observable<Pessoa> {
    return this.http.put<Pessoa>(this.pessoaApiUrl, pessoa);
  }

  deletePessoa(id: number): Observable<any> {
    return this.http.delete<any>(`${this.pessoaApiUrl}/${id}`);
  }
}