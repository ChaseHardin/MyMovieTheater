import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {SuiModule} from 'ng2-semantic-ui/ng2-semantic-ui';

import { AppComponent }  from './app.component';

@NgModule({
  declarations: [ AppComponent ],
  imports:      [ BrowserModule ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
