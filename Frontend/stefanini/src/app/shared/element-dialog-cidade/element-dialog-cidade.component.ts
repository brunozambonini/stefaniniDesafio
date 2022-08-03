import {Component, Inject, OnInit} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Cidade } from 'src/app/models/Cidade';

@Component({
  selector: 'app-element-dialog-cidade',
  templateUrl: './element-dialog-cidade.component.html',
  styleUrls: ['./element-dialog-cidade.component.scss']
})
export class ElementDialogCidadeComponent implements OnInit {
  element!: Cidade;
  isChange!: boolean;

  constructor(
    @Inject(MAT_DIALOG_DATA) 
    public data: Cidade,
    public dialogRef: MatDialogRef<Cidade>,
  ) {}
  
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
}

