<template>
    <div>
        <nav>
            <div class="nav navbar" id="nav-tab" role="tablist">
                <button class="nav-link active nav-first" id="nav-users-tab" data-bs-toggle="tab" data-bs-target="#nav-users"
                    type="button" role="tab" aria-controls="nav-home" aria-selected="true">USERS</button>
                <button class="nav-link" id="nav-categories-tab" data-bs-toggle="tab" data-bs-target="#nav-categories"
                    type="button" role="tab" aria-controls="nav-profile" aria-selected="false">CATEGORIES</button>
                <button class="nav-link nav-last" id="nav-attributes-tab" data-bs-toggle="tab" data-bs-target="#nav-attributes"
                    type="button" role="tab" aria-controls="nav-contact" aria-selected="false">ATTRIBUTES</button>
            </div>
        </nav>
        <div class="tab-content" id="nav-tabContent">
            <div class="tab-pane fade show active" id="nav-users" role="tabpanel" aria-labelledby="nav-users-tab">
                <users-tab /></div>
            <div class="tab-pane fade" id="nav-categories" role="tabpanel" aria-labelledby="nav-categories-tab">
                <categories-tab /></div>
            <div class="tab-pane fade" id="nav-attributes" role="tabpanel" aria-labelledby="nav-attributes-tab">
                <attributes-tab /></div>
        </div>
    </div>
</template>

<script>
import UsersTab from "@/components/admin/UsersTab";
import CategoriesTab from "@/components/admin/CategoriesTab";
import AttributesTab from "@/components/admin/AttributesTab";
export default {
    name: "AdminView",
    components: {
        UsersTab,
        CategoriesTab,
        AttributesTab
    },
    data() {
        return {

        }
    },
    methods: {

    },
    mounted() {

    }
}
</script>

<style scoped>

.nav-link.active {
    background-color: var(--main-green) !important;
    color: var(--main-white) !important;
}
.navbar {
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: center;
}

.nav-last {
    border-top-right-radius: 0.5rem !important;
    border-bottom-right-radius: 0.5rem !important;
}

.nav-first {
    border-top-left-radius: 0.5rem !important;
    border-bottom-left-radius: 0.5rem !important;
}
.nav-link {
    color: var(--main-black);
    background-color: var(--main-white);
    font-size: 3rem;
    border-color: var(--main-black);
    border-radius: 0;
    padding: 1rem 2rem;
}




</style>