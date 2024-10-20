context('SideMenu', () => {
    beforeEach(() => {
        cy.visit('http://localhost:8080')
    })

    it('is visits the inventory page via logo', () => {
        cy.get('#imgLogo').click()
        cy.get('#inventoryTitle').contains('Inventory Dashboard')
    })
})