import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts!: BalanceOverall;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<BalanceOverall>(baseUrl + 'balanceoverall').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface BalanceOverall {
  accountType1: number;
  accountType2: number;
  accountType3: number;
  accountType4: number;
  accountType5: number;
}
