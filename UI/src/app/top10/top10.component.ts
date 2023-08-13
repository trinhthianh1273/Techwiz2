import { Component } from '@angular/core';
import { Top10 } from '../model/top10';
import { PeriodicElement } from '../model/periodic-element';

@Component({
  selector: 'app-top10',
  templateUrl: './top10.component.html',
  styleUrls: ['./top10.component.css']
})



export class Top10Component {
  dataSource: Top10[] = [
    {position: 1, name: 'Cristiano Ronaldo', games: 1173, average: 0.72},
    {position: 2, name: 'Lionel Messi', games: 1032, average: 0.79},
    {position: 3, name: 'Josef Bican', games: 530, average: 1.52},
    {position: 4, name: 'Pelé', games: 812, average: 0.94},
    {position: 5, name: 'Romário', games: 963, average: 0.78},
    {position: 6, name: 'Ferenc Puskás', games: 746, average: 0.99},
    {position: 7, name: 'Gerd Müller', games: 793, average: 0.93},
    {position: 8, name: 'Jimmy Jones', games: 614, average: 1.05},
    {position: 9, name: 'Abe Lenstra', games: 650, average: 0.99},
    {position: 10, name: 'Eusébio', games: 648, average: 0.96},
  ];
  dataSource1:PeriodicElement[]=[
    {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
  ]
  // displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  displayedColumns: string[] = ['position', 'name', 'games', 'average'];
}
