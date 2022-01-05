describe("Test resister page", () => {
  it("invalid registration due to duplication", () => {
    cy.visit("/register")
      .get(".register form input")
      .first()
      .type("martin.petrov033@gmail.com")
      .get(".register form input")
      .eq(1)
      .type("Martin")
      .get(".register form input")
      .eq(2)
      .type("martin11")
      .get(".register form input")
      .eq(3)
      .click()
      .get(".swal2-container *")
      .should(($elementsArray) => {
        expect($elementsArray.length).to.be.greaterThan(0);
      });
  });
});
