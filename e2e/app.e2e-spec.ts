import { NetcoreangularcliPage } from './app.po';

describe('netcoreangularcli App', () => {
  let page: NetcoreangularcliPage;

  beforeEach(() => {
    page = new NetcoreangularcliPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
