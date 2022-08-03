import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { Pessoa } from 'src/app/models/Pessoa';
import { PessoaService } from 'src/app/services/pessoa.service';
import { CidadeService } from 'src/app/services/cidade.service';
import { ElementDialogComponent } from 'src/app/shared/element-dialog/element-dialog.component';

@Component({
  selector: 'app-pessoa',
  templateUrl: './pessoa.component.html',
  styleUrls: ['./pessoa.component.scss'],
  providers: [PessoaService, CidadeService],
})
export class PessoaComponent implements OnInit {
  @ViewChild(MatTable)
  table!: MatTable<any>;

  displayedColumns: string[] = ['nome', 'cpf', 'idade', 'cidade','actions'];
  dataSource!: Pessoa[];

  constructor(
    public dialog: MatDialog,
    public pessoaService: PessoaService
    ) {
        this.pessoaService.getPessoas()
          .subscribe((data: Pessoa[]) => {
            this.dataSource = data;
          });
     }

  ngOnInit(): void {
  }

  openDialog(element: Pessoa | null): void{
    const dialogRef = this.dialog.open(ElementDialogComponent, {
      width: '250px',
      data: element === null ? {
        id: 0,
        nome: '',
        cpf: '',
        idade: 0,
      } : {
        id: element.id,
        nome: element.nome,
        cpf: element.cpf,
        idade: element.idade,
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result !== undefined){
        if (this.dataSource.map(p => p.id).includes(result.id)) {
          const pessoaId = result.id;

          this.pessoaService.editPessoa(result)
            .subscribe((data: Pessoa) => {
              const index = this.dataSource.findIndex(p => p.id === pessoaId);
              this.dataSource[index] = result;
              this.table.renderRows();
            });
        } else{
          console.log(result);
          this.pessoaService.createPessoa(result)
            .subscribe((data: Pessoa) => {
              this.dataSource.push(result);
              this.table.renderRows();
            })
        }
      }
    });
  }

  deleteElement(id: number): void {
    this.pessoaService.deletePessoa(id)
      .subscribe(() => {
        this.dataSource = this.dataSource.filter(p => p.id !== id)
      });
  }

  editElement(element: Pessoa | null): void{
    this.openDialog(element);
  }

  applyFilter(filterValue: string){
    console.log(filterValue)
  }

  onKey(value: string) {
    if(value.length > 2){
      this.pessoaService.getPessoasFiltering(value)
          .subscribe((data: Pessoa[]) => {
            this.dataSource = data;
          });
    }else if(value.length === 0){
      this.pessoaService.getPessoas()
          .subscribe((data: Pessoa[]) => {
            this.dataSource = data;
          });
    }
  }
}
