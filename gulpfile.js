/// <vs />
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var watch = require('gulp-watch');
var gulpFilter = require('gulp-filter');
var mainBowerFiles = require('main-bower-files');
var minifyCss = require('gulp-minify-css');
var concatCss = require('gulp-concat-css');
var rename = require('gulp-rename');
var ts = require('gulp-typescript');

var projectName = 'ScoreTracker.Web';

var config = {
    src: mainBowerFiles(),
    js: [
        'bower_components/jquery/jquery.js',
        'bower_components/angular/angular.js',
        'bower_components/angular-route/angular-route.js',
        'bower_components/angular-toastr/dist/angular-toastr.js',
        'bower_components/bootstrap/dist/js/bootstrap.js'
    ],
    ts: [
        projectName + '/app/*.ts',
        projectName + '/Scripts/typings/**/*.d.ts',
        projectName + '/app/**/*.ts'
    ],
    css: [
        'bower_components/bootstrap/dist/css/*.min.css',
        'bower_components/bootstrap/dist/css/*.map',
        'bower_components/angular-toastr/dist/*.min.css'
    ]
}


var jsFilter = gulpFilter('*.js');
var cssFilter = gulpFilter('*.css');
var fontFilter = gulpFilter(['*.eot', '*.woff', '*.svg', '*.ttf']);

gulp.task('clean-js', function() {
    del.sync([projectName + '/app/all.min.js']);
});

gulp.task('clean-css', function () {
    del.sync([projectName + "/content/*.css", projectName + "/Content/*.map"]);
});

gulp.task('clean-ts', function () {
    del.sync([projectName + '/app/app.js']);
});

gulp.task('js', ['clean-js'], function() {

    gulp.src(config.js)
        // JS
        //.pipe(jsFilter)
        //.pipe(uglify())
        .pipe(concat('all.min.js'))
        .on('error', swallowError)
        .pipe(gulp.dest(projectName + '/app/'));
    //.pipe(jsFilter.restore());

    //// CSS
    //.pipe(cssFilter)
    //.pipe(gulp.dest(projectName + '/Content/'))
    ////.pipe(minifyCss())
    ////.pipe(concatCss("bundle.css"))
    ////.pipe(gulp.dest('ScoreTracker.Web/Content/'))
    //.pipe(cssFilter.restore());


});

gulp.task('css', ['clean-css'], function() {
    gulp.src(config.css)
        .pipe(gulp.dest(projectName + '/Content/'));
});

gulp.task('ts', ['clean-ts'], function() {
    gulp.src(config.ts)
        .pipe(ts({
            decarationFiles: true,
            noExternalResolve: true,
        }))
        .on('error', swallowError)
        .pipe(concat('app.js'))
        .pipe(gulp.dest(projectName + '/app'));
});


//gulp.task('bower', function() {
//    return bower()
//        .pipe(gulp.dest(config.bowerDir));
//});

gulp.task('watch', function () {
    console.log(typeof (config.ts));
    watch(config.js, function() {
        gulp.start('js');
    });
    watch(config.ts, function() {
        gulp.start('ts');
    });
    watch(config.css, function() {
        gulp.start('css');
    });

    //gulp.watch(config.js.concat(config.ts, config.css), ['js', 'ts', 'css']);
    //watch(config.js.concat(config.ts, config.css)), function () {
    //    console.log('Trigger');
    //    config = buildConfig();
    //    gulp.start('default');
//};
});

gulp.task('default', ['js', 'ts', 'css'], function () { });

function swallowError(error) {

    //If you want details of the error in the console
    console.log(error.toString());

    this.emit('end');
}

