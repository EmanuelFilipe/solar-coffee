context('SideMenu', () => {
    beforeEach(() => {
        cy.visit('http://localhost:8080')
    })

    it('is the inventory page', () => {
        cy.get('#inventoryTitle').contains('Inventory Dashboard')
    })

    it('has buttons to add inventory and receive shipment', () => {
        cy.get('#addNewItem > .solar-button').contains('Add New Item')
        cy.get('#receiveShipmentModal > .solar-button').contains('Receive Shipment')
    })

    it('has add new item modal on click', () => {
        cy.get('#addNewItem > .solar-button').click()
        cy.get('#modalTitle').contains('Add New Product')
        cy.get('[aria-label="close modal"] > .solar-button').click()
    })

    it('adding new product and closing modal before save does not add new product', () => {
        cy.get('#addNewItem > .solar-button').click()
        cy.get('#isTaxable').click()
        cy.get('#productName').clear()
        cy.get('#productName').type('Test Product from cy', { delay: 80})
        cy.get('#productDesc').clear()
        cy.get('#productDesc').type('a great new test product for sale  - from cy', { delay: 60})
        cy.get('#productPrice').clear()
        cy.get('#productPrice').type('120', { delay: 50})
        cy.get('[aria-label="close modal"] > .solar-button').click()
    })

    it('adding new product and saving adds new product', () => {
        cy.get('#addNewItem > .solar-button').click()
        cy.get('#isTaxable').click()
        cy.get('#productName').clear()
        cy.get('#productName').type('Test Product - from cy', { delay: 80})
        cy.get('#productDesc').clear()
        cy.get('#productDesc').type('a great new test product for sale  - from cy', { delay: 60})
        cy.get('#productPrice').clear()
        cy.get('#productPrice').type('120', { delay: 50})
        cy.get('[aria-label="save new item"] > .solar-button').click()
        cy.get('table').contains('td', 'Test Product - from cy')
    })

    it('archives new product', () => {
        cy.get('#addNewItem > .solar-button').click()
        cy.get('#isTaxable').click()
        cy.get('#productName').clear()
        cy.get('#productName').type('Test Product to archive - from cy', { delay: 80})
        cy.get('#productDesc').clear()
        cy.get('#productDesc').type('test arqhive - from cy', { delay: 60})
        cy.get('#productPrice').clear()
        cy.get('#productPrice').type('4000', { delay: 50})
        cy.get('[aria-label="save new item"] > .solar-button').click()
        cy.wait(2000)
        cy.get('#inventoryTable > tbody > tr > td > div').last().click()
    })
})