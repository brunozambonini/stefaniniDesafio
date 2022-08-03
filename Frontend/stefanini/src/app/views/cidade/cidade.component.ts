import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { Cidade } from 'src/app/models/Cidade';
import { CidadeService } from 'src/app/services/cidade.service';
import { ElementDialogCidadeComponent } from 'src/app/shared/element-dialog-cidade/element-dialog-cidade.component';

@Component({
  selector: 'app-cidade',
  templateUrl: './cidade.component.html',
  styleUrls: ['./cidade.component.scss'],
  providers: [CidadeService]
})
export class CidadeComponent implements OnInit {
  @ViewChild(MatTable)
  table!: MatTable<any>;

  displayedColumns: string[] = ['nome', 'uf', 'actions'];
  dataSource!: Cidade[];

  constructor(
    public dialog: MatDialog,
    public cidadeService: CidadeService
    ) {
        this.cidadeService.getCidades()
          .subscribe((data: Cidade[]) => {
            this.dataSource = data;
          });
     }

  ngOnInit(): void {
  }

  openDialog(element: Cidade | null): void{
    const dialogRef = this.dialog.open(ElementDialogCidadeComponent, {
      width: '250px',
      data: element === null ? {
        id: 0,
        nome: '',
        uf: '',
      } : {
        id: element.id,
        nome: element.nome,
        uf: element.uf,
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result !== undefined){
        if (this.dataSource.map(p => p.id).includes(result.id)) {
          const cidadeId = result.id;

          this.cidadeService.editCidade(result)
            .subscribe((data: Cidade) => {
              const index = this.dataSource.findIndex(p => p.id === cidadeId);
              this.dataSource[index] = result;
              this.table.renderRows();
            });
        } else{
          console.log(result);
          this.cidadeService.createCidade(result)
            .subscribe((data: Cidade) => {
              this.dataSource.push(result);
              this.table.renderRows();
            })
        }
      }
    });
  }

  deleteElement(id: number): void {
    this.cidadeService.deleteCidade(id)
      .subscribe(() => {
        this.dataSource = this.dataSource.filter(p => p.id !== id)
      });
  }

  editElement(element: Cidade | null): void{
    this.openDialog(element);
  }

  applyFilter(filterValue: string){
    console.log(filterValue)
  }

  onKey(value: string) {
    if(value.length > 2){
      this.cidadeService.getCidadesFiltering(value)
          .subscribe((data: Cidade[]) => {
            this.dataSource = data;
          });
    }else if(value.length === 0){
      this.cidadeService.getCidades()
          .subscribe((data: Cidade[]) => {
            this.dataSource = data;
          });
    }
  }
}
