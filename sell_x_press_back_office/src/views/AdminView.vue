<template>
    <div>
        <nav>
            <div class="nav nav-tabs" id="nav-tab" role="tablist">
                <button class="nav-link active" id="nav-users-tab" data-bs-toggle="tab" data-bs-target="#nav-users" type="button" role="tab" aria-controls="nav-home" aria-selected="true">Users</button>
                <button class="nav-link" id="nav-categories-tab" data-bs-toggle="tab" data-bs-target="#nav-categories" type="button" role="tab" aria-controls="nav-profile" aria-selected="false">Categories</button>
                <button class="nav-link" id="nav-attributes-tab" data-bs-toggle="tab" data-bs-target="#nav-attributes" type="button" role="tab" aria-controls="nav-contact" aria-selected="false">Attributes</button>
            </div>
        </nav>
            <div class="tab-content" id="nav-tabContent">
            <div class="tab-pane fade show active" id="nav-users" role="tabpanel" aria-labelledby="nav-users-tab"><users-tab/></div>
            <div class="tab-pane fade" id="nav-categories" role="tabpanel" aria-labelledby="nav-categories-tab"><categories-tab/></div>
            <div class="tab-pane fade" id="nav-attributes" role="tabpanel" aria-labelledby="nav-attributes-tab"><attributes-tab/></div>
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

</style>