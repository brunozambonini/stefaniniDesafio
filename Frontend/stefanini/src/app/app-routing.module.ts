import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CidadeComponent } from './views/cidade/cidade.component';
import { PessoaComponent } from './views/pessoa/pessoa.component';

const routes: Routes = [
  {
    path: '',
    component: PessoaComponent
  },
  {
    path: 'cidades',
    component: CidadeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
