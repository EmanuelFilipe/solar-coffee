import { mount } from '@vue/test-utils'
import SideMenu from '@/components/SideMenu.vue'

describe('SideMenu.vue', () => {
    it('renders correct numbers of button links', () => {
        const wrapper = mount(SideMenu, {
            propsData: {},
            stubs: ["router-link"],
            slots: {}
        })

        expect(wrapper.findAll('button').length).toBe(5)
    })
})