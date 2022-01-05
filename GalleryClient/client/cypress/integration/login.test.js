describe("Test login page", () => {
  beforeEach(() => {
    cy.visit("/login")
      .get(".login form input")
      .first()
      .type("Martin")
      .get(".login form input")
      .eq(1)
      .type("martin11")
      .get(".login form input")
      .eq(2)
      .click();
  });

  it("loads all links for authorised users", () => {
    cy.visit("/")
      .get(".links div a")
      .should(($linksArray) => {
        expect($linksArray.length).to.be.equal(5);
      });
  });

  it("loads all links for unauthorised users", () => {
    cy.visit("/")
      .get(".links div a")
      .eq(4)
      .click()
      .get(".links div a")
      .should(($linksArray) => {
        expect($linksArray.length).to.be.equal(2);
      });
  });
});
