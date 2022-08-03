import {Component, Inject, OnInit} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Cidade } from 'src/app/models/Cidade';
import { Pessoa } from 'src/app/models/Pessoa';
import { CidadeService } from 'src/app/services/cidade.service';

@Component({
  selector: 'app-element-dialog',
  templateUrl: './element-dialog.component.html',
  styleUrls: ['./element-dialog.component.scss'],
  providers: [CidadeService]
})
export class ElementDialogComponent implements OnInit {
  element!: Pessoa;
  isChange!: boolean;

  cidades!: Cidade[];
  selectedCity!: number;

  constructor(
    @Inject(MAT_DIALOG_DATA) 
    public data: Pessoa,
    public dialogRef: MatDialogRef<Pessoa>,
    public cidadeService: CidadeService,
  ) {
    this.cidadeService.getCidades()
            .subscribe((data: Cidade[]) => {
              this.cidades = data;
            });
  }
  
  ngOnInit(): void {
    if(this.data.id != 0){
      this.isChange = true;
    } else{
      this.isChange = false;
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  compareObjects(o1: any, o2: any): boolean {
    return o1.name === o2.name && o1.id === o2.id;
  }
}
